import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { LoginService } from '../login.service';

@Component({
  selector: 'app-discussion',
  templateUrl: './discussion.component.html',
  styleUrls: ['./discussion.component.scss']
})
export class DiscussionComponent implements OnInit {

  comments: any;
  disscussionID:string = "";
  discussion: any;
  subject: any;
  displaySpoilers: any = false;
  user: any;

  newComment: any = {
    discussionid: 0,
    username: "",
    text: "",
    isspoiler: false
  };

  constructor(private _login:LoginService,private router :ActivatedRoute) { }

  ngOnInit(): void {

    this.disscussionID =  this.router.snapshot.params.id;
    this.newComment.discussionid = this.router.snapshot.params.id;
    this.displayInput();
    this.getComments();
    this._login.getCurrentDiscussion(this.disscussionID).subscribe(data => {
      console.log(data);
      this.discussion = data;
      this.subject = this.discussion.subject;
    });
  }

  async getComments() {
    setTimeout(() => {
      this._login.getDiscussionComments(this.disscussionID).subscribe(data =>{ 
        console.log(data);
        this.comments = data;
      });
    }, 1000);
  }

  displayInput(){
    if(localStorage.getItem("loggedin"))
    {
      this.user = localStorage.getItem("loggedin");
      this.newComment.username= JSON.parse(this.user).username;
      console.log("User Logged In");
    }else{
      console.log("Hide inputs");
    }
  }

  getDicussionID(){
    console.log("Dicussion ID " +this.disscussionID);
    return this.disscussionID;
  }

  postComment(){
    if(this.isEmpty(this.newComment.text)){
      console.log("Please enter a comment");
    }else{
      this._login.postComment(this.newComment).subscribe(data => console.log(data));
      this.getComments();
    }
    console.log(this.newComment);
  }

  showSpoilers() {
    this.displaySpoilers = true;
    console.log(this.displaySpoilers);
  }

  spoilersShown() {
    return this.displaySpoilers;
  }

  isEmpty(testSTR:string){
    return (testSTR == "");
  }

}