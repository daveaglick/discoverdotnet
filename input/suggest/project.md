Title: Suggest A Project
---
To suggest a new project, submit a PR to the [discoverdotnet](https://github.com/daveaglick/discoverdotnet) project manually or by clicking on this link: [https://github.com/daveaglick/discoverdotnet/new/master/input/data/projects](https://github.com/daveaglick/discoverdotnet/new/master/input/data/projects). You should add a new YAML file with a `.yml` extension that contains the following properties, all of which are optional unless indicated.

**Source**: The location of the source code for the project. Should contain a full URL like `https://github.com/daveaglick/discoverdotnet`.  
**Title**: The title of the project. If a GitHub repository was provided for the source and this is omitted, the title from the repository metadata will be used. 
**Image**: A URL to an image or logo of the project. 
**Description**: A short description of the project. If a GitHub repository was provided for the source and this is omitted, the description from the repository metadata will be used.  
**Website**: The primary website for the project. If a GitHub repository was provided for the source and this is omitted, the website from the repository metadata will be used.  
**Docs**: The primary documentation site for the project.  
**Chat**: A link to the chat room for the project if one exists.  
**Donations**: A link to provide donations to the project.  
**Language**: The primary language of the project (for example, `C#`, `F#`, or `VB`).  
**NuGet**: A single primary package for the project. Many projects have multiple NuGet packages, but for now this should just be the most important of them.  
**Twitter**: The Twitter handle of the project without the `@` symbol.  
**Platform**: `true` if the project is considered part of the .NET platform, otherwise this can be omitted.  
**Foundation**: `true` if the project is a member of the .NET Foundation. In most cases this can be automatically determined based on the projects listed at the .NET Foundation website and this property isn't needed.  
**Microsoft**: `true` if the project is developed or sponsored by Microsoft. In most cases this can be automatically determined based on the owner of the repository and this property isn't needed.  
**Tags**: A list of tags for the project. Care should be taken to use already existing tags where practical.  

For example, here's what the YAML file for [Wyam](https://wyam.io) looks like:

```yaml
Source:  https://github.com/Wyamio/Wyam
Language: C#
Tags:
  - Static Site Generation
NuGet: Wyam
Twitter: wyamio
Chat: https://gitter.im/Wyamio/Wyam
```