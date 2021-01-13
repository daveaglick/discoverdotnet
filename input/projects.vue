<template>
    <b-card no-body header-bg-variant="secondary" bg-variant="light" class="mb-3">
        <div slot="header">
            <h3>
                <span class="accent-primary">Projects</span>
            </h3>              
            <hr/>
            <div class="accent-script">{{ projectKeys.length }} total projects indexed!</div>
        </div>
        <b-card-body>
            <p class="text-left"><a href="/projects">All Projects <span class="fa fa-long-arrow-alt-right"></span></a></p>   
            <p class="text-left"><a href="/projects/?filter-netplatform">All .NET Platform Projects <span class="fa fa-long-arrow-alt-right"></span></a></p>         
            <p class="text-left"><a href="/projects/?filter-microsoft">All Microsoft Projects <span class="fa fa-long-arrow-alt-right"></span></a></p>         
            <p class="text-left"><a href="/projects/?filter-netfoundation">All .NET Foundation Projects <span class="fa fa-long-arrow-alt-right"></span></a></p>      
        </b-card-body>
    </b-card>
</template>

<script>
    module.exports = {
        props: [
            "projectKeys"
        ]
    }
</script>