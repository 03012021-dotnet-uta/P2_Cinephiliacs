import { HttpClient } from '@angular/common/http';
import { analyzeAndValidateNgModules } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpService} from '../http.service';
import { LoginService } from '../login.service';
import { Review } from '../models';

@Component({
  selector: 'app-movie',
  templateUrl: './movie.component.html',
  styleUrls: ['./movie.component.scss']
})
export class MovieComponent implements OnInit {

  reviewScoreSum:number = 0;
  reviewScore:number = 0;
  selectedMovie: any;
  movieID: any;
  discussions: any;
  reviews: Review[] = [];
  input:any;
  user:any;
  movieFollowed: any = false;

  reviewPage: number = 1;
  reviewSortOrder: string = "ratingdsc";

  caninput:boolean = false;

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

  constructor(private router :ActivatedRoute, private _http: HttpService, private _login: LoginService) { }

  ngOnInit(): void {
    console.log(this.router.snapshot.params);
    this.inputFields();
    this._login.getTopics().subscribe(data => {
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
    this.showDiscussion();

    //Movie Reviews
    this.loadReviews(this.reviewPage);

    //saving a reference to the database of movies interacted with
    this._login.postMovieId(this.movieID).subscribe(data => console.log("submitted"));
  }

  loadReviews(page:number) {
    this._login.getReviewsPage(this.movieID, page, this.reviewSortOrder).subscribe((data:Review[]) => {
      if(data.length == 0)
      {
        this.setLastPage();
        this.reviewPage = page - 1;
      }
      else
      {
        data.forEach((review:Review) => {
          console.log(review);
          this.reviews.push(review);
          this.reviewScoreSum += Number(review.rating);
        });
          this.reviewScore = this.reviewScoreSum/this.reviews.length;
          console.log(this.reviewScore);
      }
    });
  }

  loadNextPage()
  {
    this.reviewPage += 1;
    this.loadReviews(this.reviewPage);
  }

  setLastPage()
  {

  }

  reloadReviews(){
    for (let pageCounter:number = 1; pageCounter <= this.reviewPage; pageCounter++) {
      setTimeout(() => { this.loadReviews(pageCounter); }, 500*pageCounter);
    }
  }

  canYouInput(): boolean {
    console.log("Can input ?" + this.caninput);
    return this.caninput;
  }

  async showDiscussion(){
    setTimeout(() => {
      this._login.getDiscussion(this.movieID).subscribe(data => {
        console.log(data);
        this.discussions = data;
      });
    }, 2000);
  }

  followMovie(){
    if(localStorage.getItem("loggedin")){
      this._login.followMovie(JSON.parse(this.user).username,this.movieID).subscribe(data => console.log("following Movie Now"));
      this.movieFollowed = true;
      setTimeout(() => {
        this.movieFollowed = false;
      }, 2000);
    }
  }

  postDiscussion(){
    if(this.submitDiscussion.topic == "" || this.submitDiscussion.subject == "")
    {
      console.log("didn't submit discussion");
    }else if(this.submitDiscussion.subject.length >= 250){
      alert("Discussion should be less than 250 Characters")
    }else{

      this._login.submitDiscussion(this.submitDiscussion).subscribe(data => console.log(data));
      this.showDiscussion();
    }
    console.log(this.submitDiscussion);
  }

  postReview(){
    if(this.sumbitReview.rating == 0 || this.sumbitReview.text == ""){
      console.log("Review Not Sumbitted");
    }else if(this.sumbitReview.text.length >= 250){
      alert("Reviews should be less than 250 Characters")
    }else{
      this._login.postReview(this.sumbitReview).subscribe(data => console.log(data));
      this.reloadReviews();
    }
    console.log(this.sumbitReview);
  }

  inputFields(){
    if(localStorage.getItem("loggedin")){
        this.caninput=true;
        console.log("userset");
        this.user = localStorage.getItem("loggedin")

        console.log(JSON.parse(this.user).username + "USER");
        console.log(this.user);
        this.submitDiscussion.username =JSON.parse(this.user).username;
        this.sumbitReview.username = JSON.parse(this.user).username;
        console.log(this.sumbitReview);

    }else{

      console.log("no User");
    }
  }

  changeReviewSortOrder(sortOrder:string)
  {
    if(sortOrder == "ratingasc" || sortOrder == "ratingdsc"
      || sortOrder == "timeasc" || sortOrder == "timedsc")
    {
      this.reviews = [];
      this.reviewPage = 1;
      this.reviewScoreSum = 0;
      this.reviewSortOrder = sortOrder;
      this.loadReviews(this.reviewPage);
    }
  }
}
