import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { AppComponent } from './app.component';


@Injectable({
  providedIn: 'root'
})
export class LoginService {

  currentUser:string ="";
  askingUser:string = "";
  connection:string ="";
  loggedIn:any;

  baseURL:string = "https://cinephiliacsapi.azurewebsites.net/";

  constructor(private http:HttpClient) { }

  createUser(newUser:string){
    return this.http.post(this.baseURL+ "user/",newUser);
  }

  loginUser(userName:string){
    this.connection =  this.baseURL +"user/" + userName;
    console.log(this.connection);
    return this.http.get(this.connection);
  }

  getURL(){
    return this.baseURL;
  }

  getTopics(){
    return this.http.get( this.baseURL + "forum/topics");
  }
  

  getDiscussion(movieId:String){
    return this.http.get( this.baseURL + "forum/discussions/"+movieId);
  }

  getReviews(movieId:String){
    return this.http.get( this.baseURL + "movie/reviews/"+movieId);
  }

  submitDiscussion(discussion:any){
    return this.http.post( this.baseURL + "forum/discussion", discussion);
  }

  postMovieId(movieID:string){
    return this.http.post( this.baseURL + "movie/" +movieID,null);
  }

  postReview(sumbitReview:any){
    return this.http.post( this.baseURL + "movie/review", sumbitReview);
  }

  getUser(username:string){
    return this.http.get( this.baseURL + "user/"+ username);
  }

  getUserReviews(username:string){
    return this.http.get( this.baseURL + "user/reviews/" + username);
  }
  getUserDiscussions(username:string){
    return this.http.get( this.baseURL + "user/discussions/" + username);
  }
  getDiscussionComments(discussionID:string){
    return this.http.get( this.baseURL + "forum/comments/" + discussionID);
  }
  getCurrentDiscussion(discussionID:string){
    return this.http.get( this.baseURL + "forum/discussion/" + discussionID);
  }

  postComment(newComment:any){
    return this.http.post( this.baseURL + "Forum/comment",newComment);
  }

}
