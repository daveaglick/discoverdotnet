using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NetlifySharp;
using Statiq.Common;

namespace DiscoverDotnet.Modules
{
    public class DeployNetlifySite : MultiConfigModule
    {
        // Config keys
        private const string AccessToken = nameof(AccessToken);
        private const string SiteId = nameof(SiteId);
        private const string ZipStream = nameof(ZipStream);

        /// <summary>
        /// Deploys the output folder to Netlify.
        /// </summary>
        /// <param name="siteId">The ID of the site to deploy.</param>
        /// <param name="accessToken">The access token to authenticate with.</param>
        public DeployNetlifySite(Config<string> siteId, Config<string> accessToken)
            : this(siteId, accessToken, Config.FromContext(ctx => ctx.FileSystem.GetOutputPath()))
        {
        }

        /// <summary>
        /// Deploys a specified folder to Netlify.
        /// </summary>
        /// <param name="siteId">The ID of the site to deploy.</param>
        /// <param name="accessToken">The access token to authenticate with.</param>
        /// <param name="directory">
        /// The directory containing the files to deploy (from the root folder, not the input folder).
        /// </param>
        public DeployNetlifySite(Config<string> siteId, Config<string> accessToken, Config<DirectoryPath> directory)
            : this(siteId, accessToken, GetZipStreamFromDirectory(directory))
        {
        }

        private static Config<Stream> GetZipStreamFromDirectory(Config<DirectoryPath> directory)
        {
            _ = directory ?? throw new ArgumentNullException(nameof(directory));

            return directory.RequiresDocument
                ? Config.FromDocument(async (doc, ctx) => GetStream(await directory.GetValueAsync(doc, ctx), ctx))
                : Config.FromContext(async ctx => GetStream(await directory.GetValueAsync(null, ctx), ctx));

            static Stream GetStream(DirectoryPath path, IExecutionContext context)
            {
                if (path == null)
                {
                    throw new ExecutionException("Invalid directory");
                }
                IFile zipFile = ZipFileHelper.CreateZipFile(context, path);
                return zipFile.OpenRead();
            }
        }

        /// <summary>
        /// Deploys a specified zip file to Netlify.
        /// </summary>
        /// <param name="siteId">The ID of the site to deploy.</param>
        /// <param name="accessToken">The access token to authenticate with.</param>
        /// <param name="zipPath">The zip file to deploy.</param>
        public DeployNetlifySite(Config<string> siteId, Config<string> accessToken, Config<FilePath> zipPath)
            : this(siteId, accessToken, GetZipStreamFromZipFile(zipPath))
        {
        }

        private static Config<Stream> GetZipStreamFromZipFile(Config<FilePath> zipPath)
        {
            _ = zipPath ?? throw new ArgumentNullException(nameof(zipPath));

            return zipPath.RequiresDocument
                ? Config.FromDocument(async (doc, ctx) => GetStream(await zipPath.GetValueAsync(doc, ctx), ctx))
                : Config.FromContext(async ctx => GetStream(await zipPath.GetValueAsync(null, ctx), ctx));

            static Stream GetStream(FilePath filePath, IExecutionContext context)
            {
                if (filePath == null)
                {
                    throw new ExecutionException("Invalid zip path");
                }
                IFile zipFile = context.FileSystem.GetFile(filePath);
                if (!zipFile.Exists)
                {
                    throw new ExecutionException("Zip file does not exist");
                }
                return zipFile.OpenRead();
            }
        }

        /// <summary>
        /// Deploys a specified zip stream to Netlify.
        /// </summary>
        /// <param name="siteId">The ID of the site to deploy.</param>
        /// <param name="accessToken">The access token to authenticate with.</param>
        /// <param name="zipStream">The zip stream to deploy (the stream will be disposed by the module).</param>
        public DeployNetlifySite(Config<string> siteId, Config<string> accessToken, Config<Stream> zipStream)
            : base(
                new Dictionary<string, IConfig>
                {
                    { SiteId, siteId ?? throw new ArgumentNullException(nameof(siteId)) },
                    { AccessToken, accessToken ?? throw new ArgumentNullException(nameof(accessToken)) },
                    { ZipStream, zipStream ?? throw new ArgumentNullException(nameof(zipStream)) }
                },
                false)
        {
        }

        protected override async Task<IEnumerable<IDocument>> ExecuteConfigAsync(IDocument input, IExecutionContext context, IMetadata values)
        {
            using (Stream zipStream = values.Get<Stream>(ZipStream))
            {
                _ = zipStream ?? throw new ExecutionException("Invalid zip stream");
                string siteId = values.GetString(SiteId) ?? throw new ExecutionException("Invalid site ID");
                string accessToken = values.GetString(AccessToken) ?? throw new ExecutionException("Invalid access token");

                context.LogDebug($"Starting Netlify deployment to {siteId}...");
                try
                {
                    NetlifyClient client = new NetlifyClient(accessToken);
                    await client
                        .UpdateSite(siteId, zipStream)
                        .WithResponseHandler(response =>
                        {
                            if (!response.IsSuccessStatusCode)
                            {
                                // TODO: Add async handlers to NetlifySharp
                                string responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                                context.LogError($"Netlify deployment error: {response.StatusCode} {responseContent}");
                                response.EnsureSuccessStatusCode();
                            }
                            else
                            {
                                context.LogDebug($"Netlify deployment success to {siteId}");
                            }
                        })
                        .SendAsync(context.CancellationToken);
                }
                catch (Exception ex)
                {
                    context.LogError($"Exception while deploying to Netlify: {ex.Message}");
                    throw;
                }
            }

            return await input.YieldAsync();
        }
    }
}
