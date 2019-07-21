var page = new Vue({
    el: "#mainRow",
    methods: {
        selectSeat: function (e) {
            var code = e.target.attributes["code"].value;
            this.selectedItems.push(code);
        },
        movieChanged(e) {
            var self = this;
            self.movieName = self.movieId != 0 ?
                e.target.selectedOptions[0].text :
                "Film Seçilmedi";
            self.soldItems.clear();
            self.selectedItems.clear();
            setTimeout(function () {
                addButtonAnimation();
            }, 500);

            $.ajax({
                method: "GET",
                url: pageOptions.getTickets + "/" + self.movieId,
                contentType: "application/json"
            }).done(function (data) {
                for (var i = 0; i < data.length; i++) {
                    self.soldItems.push(data[i]);
                }
            });
        },
        getSelected(number) {
            return this.selectedItems.includes(number);
        },
        getSold(number) {
            return this.soldItems.includes(number);
        },
        savePlaces() {
            var self = this;
            if (self.selectedItems.length <= 0) {
                alert("Film ya da koltuk seçmediniz");
                return;
            }
            var ticket = {
                movieId: this.movieId,
                seats: this.selectedItems
            }
            $.ajax({
                url: pageOptions.saveplaces,
                method: "POST",
                data: JSON.stringify(ticket),
                contentType: "application/json"
            }).done(function (data) {
                if (data) {
                    for (var i = 0; i < self.selectedItems.length; i++) {
                        self.soldItems.push(self.selectedItems[i]);
                    }
                    self.selectedItems.clear();
                }
            });
        }
    },
    data: function () {
        return {
            movies: [],
            movieId: null,
            movieName: null,
            selectedItems: [],
            soldItems: []
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