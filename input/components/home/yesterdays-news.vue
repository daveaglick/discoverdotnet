<template>
    <div class="mb-5">
        <div class="text-center">            
            <h3>
                <span class="accent-secondary">Yesterday's</span>
                <span class="accent-primary">News</span>
                
                <!-- This adds extra height to line up the hr -->
                <span class="h1"></span>
            </h3>
            <hr/>
            <p class="small">A roundup of blog posts, podcasts, and more from the previous day.</p>
        </div>

        <b-card class="mb-4">
            <div class="card-corner card-corner-dark"><i class="fa fa-pencil-alt"></i></div>
            <div class="font-weight-bold"><a href="#">Post title</a></div>
            <div class="small">Friday, January 12, 2018</div>
            <div><a href="#">Name (or link?) of the blog</a></div>                
        </b-card>
        <b-card class="mb-4">
            <div class="card-corner card-corner-dark"><i class="fa fa-headphones"></i></div>
            <div class="font-weight-bold"><a href="#">Podcast title</a></div>
            <div class="small">Friday, January 12, 2018</div>
            <div><a href="#">Name (or link?) of the blog</a></div>                
        </b-card>
        <b-card class="mb-4">
            <div class="card-corner card-corner-dark"><i class="fa fa-pencil-alt"></i></div>
            <div class="font-weight-bold"><a href="#">Post title</a></div>
            <div class="small">Friday, January 12, 2018</div>
            <div><a href="#">Name (or link?) of the blog</a></div>                
        </b-card>
        <b-card class="mb-4">
            <div class="card-corner card-corner-dark"><i class="fa fa-pencil-alt"></i></div>
            <div class="font-weight-bold"><a href="#">Post title</a></div>
            <div class="small">Friday, January 12, 2018</div>
            <div><a href="#">Name (or link?) of the blog</a></div>                
        </b-card>

        <p><a href="/blogs">More Blogs</a></p>
        <p><a href="/broadcasts">More Broadcasts</a></p>
    </div>
</template>


<script>
module.exports = {
    data: function() {
        return {}
    }
}
</script>