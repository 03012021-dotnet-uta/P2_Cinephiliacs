import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpService} from '../http.service';

@Component({
  selector: 'app-movie',
  templateUrl: './movie.component.html',
  styleUrls: ['./movie.component.scss']
})
export class MovieComponent implements OnInit {

  selectedMovie: any;
  movieID: any;
  discussions: any;
  reviews: any;


  constructor(private router :ActivatedRoute, private _http: HttpService,private http: HttpClient) { }

  ngOnInit(): void {
    console.log(this.router.snapshot.params);
    //will get the details of the movie from the IMDB API
    this.movieID = this.router.snapshot.params.id;
    this._http.getMovie(this.movieID).subscribe(data => {
      this.selectedMovie = data;
      console.log("this is movies now just so you know");
      console.log(this.selectedMovie);
    });
    //Will get the discussions for the movie
    this.http.get("https://cinephiliacsapi.azurewebsites.net/forum/discussions/"+this.movieID).subscribe(data => {
      console.log(data);
      this.discussions = data;
    });

    //Movie Reviews
    this.http.get("https://cinephiliacsapi.azurewebsites.net/movie/reviews/"+this.movieID).subscribe(data => {
      console.log(data);
      this.reviews = data;
    });

    //saving a reference to the database of movies interacted with
    this.http.post("https://cinephiliacsapi.azurewebsites.net/movie/" +this.movieID,null);
  }


  
}
