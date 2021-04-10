import { HttpClient } from '@angular/common/http';
import { analyzeAndValidateNgModules } from '@angular/compiler';
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
  input:any;

  sumbitReview: any ={
    rating:0,
    movieid:this.router.snapshot.params.id,
    username:0,
    text:""
  }

  submitDiscussion: any ={
    movieid:this.router.snapshot.params.id,
    topic:"",
    username:"",
    subject:""
  }

  topics:any;

  constructor(private router :ActivatedRoute, private _http: HttpService,private http: HttpClient) { }

  ngOnInit(): void {
    console.log(this.router.snapshot.params);
    this.inputFeilds();

    this.http.get("https://cinephiliacsapi.azurewebsites.net/forum/topics").subscribe(data => {
      console.log(data);
      this.topics = data;
  });
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
    this.http.post("https://cinephiliacsapi.azurewebsites.net/movie/" +this.movieID,null).subscribe(data => console.log("submitted"));
  }

  showReview(){

  }

  showDiscussion(){

  }

  postDiscussion(){
    if(this.submitDiscussion.topic == "" || this.submitDiscussion.subject == "")
    {
      console.log("didn't submit discussion");
    }else{
      this.http.post("https://cinephiliacsapi.azurewebsites.net/forum/discussion", this.submitDiscussion).subscribe(data => console.log(data));
    }
    console.log(this.submitDiscussion);
  }

  postReview(){
    if(this.sumbitReview.rating ==0 || this.sumbitReview.text ==""){
      console.log("Review Not Sumbitted");
    }else{
      this.http.post("https://cinephiliacsapi.azurewebsites.net/movie/review", this.sumbitReview).subscribe(data => console.log(data));
    }
    console.log(this.sumbitReview);
  }
  
  inputFeilds(){
    if(localStorage.getItem("loggedin")){
      console.log("userset");
      this.submitDiscussion.username = localStorage.getItem("loggedin");
    this.sumbitReview.username = localStorage.getItem("loggedin");
     console.log(this.sumbitReview);
     
    }else{
      
      console.log("user isn't set");
      this.input = document.getElementById("input");
      this.input.style.display = "none";
    }
  }


  
}
