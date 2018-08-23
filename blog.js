$('#searchButton').click(function (event) {
    $.ajax({
        type: 'GET',
        URL: 'http://localhost:56878/Blog/ViewSearchResults/' + $('#searchTerm').val(),
        success: function (blogArray) {
            var blogDiv = $("#blogDisplay");

            $.each(blogArray, function (index, blog) {
                var blogInfo = "<p>";
                blogInfo += blog.Title + "<br />";
                blogInfo += blog.Content + "<br />";
                blogInfo += blog.SearchTags + "<br />";

                blogDiv.append(blogInfo);
            });
        },

        error: function () {
            alert("No blogs found with that tag");
        }
    })
});

<div class="form-group">
    <form>
        Search by tag: <br />
        <input type="text" name="searchTerm" id="searchTerm" required/><br />
        <button class="submit" id="searchButton">Search</button>
    </form>
</div>