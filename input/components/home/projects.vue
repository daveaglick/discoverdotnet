<template>
    <div class="mb-5">
        <div class="text-center mb-5">            
            <h3>
                <span class="accent-primary">Projects</span>
                
                <!-- This adds extra height to line up the hr -->
                <span class="h1"></span>
            </h3>
            <hr/>
            <p class="accent-script">{{ projectKeys.length }} total projects indexed!</p>
            <p class="text-left"><a href="/projects">All Projects <span class="fa fa-long-arrow-alt-right"></span></a></p>   
            <p class="text-left"><a href="/projects/?filter-netplatform">All .NET Platform Projects <span class="fa fa-long-arrow-alt-right"></span></a></p>         
            <p class="text-left"><a href="/projects/?filter-microsoft">All Microsoft Projects <span class="fa fa-long-arrow-alt-right"></span></a></p>         
            <p class="text-left"><a href="/projects/?filter-netfoundation">All .NET Foundation Projects <span class="fa fa-long-arrow-alt-right"></span></a></p>      
        </div>
    </div>
</template>

<script>
    module.exports = {
        props: [
            "projectKeys"
        ]
    }
</script>