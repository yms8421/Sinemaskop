var page = new Vue({
    el: "#mainRow",
    methods: {
        selectSeat: function (e) {
            var code = e.target.attributes["code"].value;
            this.selectedItems.push(code);
        },
        movieChanged(e) {
            this.movieName = this.movieId != 0 ?
                e.target.selectedOptions[0].text :
                "Film Seçilmedi";
        },
        getSelected(number) {
            return this.selectedItems.includes(number);
        },
        selectMovie() {
            alert("Seçtim");
        },
        savePlaces() {
            $.ajax({
                url: pageOptions.saveplaces,
                method: "POST",
                data: JSON.stringify(this.selectedItems),
                contentType: "application/json"
            }).done(function (data) {
                console.log(data)
            });
        }
    },
    data: function () {
        return {
            movies: [],
            movieId: null,
            movieName: null,
            selectedItems: []
        }
    },
    mounted() {
        var self = this;
        $.ajax({
            url: pageOptions.getMovies,
            method: "GET",
            contentType: "application/json"
        }).done(function (data) {
            for (var i = 0; i < data.length; i++) {
                self.movies.push(data[i]);
            }

            setTimeout(function () {
                self.message = "Yüklendi";
            }, 1500);
        }).fail(function (error) {
            console.log("Hata : " + error);
        });
        self.movieId = 0;
        self.movieName = "Film Seçilmedi";
    }
});