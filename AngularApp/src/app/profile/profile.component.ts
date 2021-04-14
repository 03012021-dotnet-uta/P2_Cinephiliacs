import { Component, Input, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User, Review, Discussion, Comment } from '../models';
import { LoginService } from '../login.service';
import { HttpService } from '../http.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  @Input() currentUser: User = {
    username:'',
    firstname:'',
    lastname:'',
    email:'',
    permissions:1
  }

  userMovieNames: string[] = [];
  userMovies: any[] = [];
  userReviews: Review[] = [];
  userDiscussions: Discussion[] = [];
  userComments: Comment[] = [];

  constructor(private _http: HttpService, private _login: LoginService) { }

  ngOnInit(): void {

    this._login.getUserMovies(this.currentUser.username).subscribe(data => {
      this.userMovieNames = data;

      this.userMovieNames.forEach(movieName => {

        this._http.getMovie(movieName).subscribe(movieData => {
          this.userMovies.push(movieData);
        });
      });
    });

    this._login.getUserDiscussions(this.currentUser.username).subscribe(data => {
      this.userDiscussions = data;
    });

    this._login.getUserComments(this.currentUser.username).subscribe(data => {
      this.userComments = data;
    });

    this._login.getUserReviews(this.currentUser.username).subscribe(data => {
      this.userReviews = data;
    });


    // this.http.get("https://cinephiliacsapi.azurewebsites.net/user/users").subscribe(data => {
    //   console.log(data);
    //   this.users=data;
    // });
  }

}
