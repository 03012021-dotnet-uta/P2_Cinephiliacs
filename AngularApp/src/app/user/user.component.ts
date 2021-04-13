import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { LoginService } from '../login.service';


@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {

  

  username: string = "";
  user: any;
  reviews: any;
  discussions: any;
  constructor(private _login:LoginService, private router: ActivatedRoute) { }

  ngOnInit(): void {
    this.username = this.router.snapshot.params.username;
    console.log(this.username);
    this._login.getUser(this.username).subscribe(data => {
      console.log(data);
      this.user = data;
  });
    this._login.getUserReviews(this.username).subscribe(data => {
      console.log(data);
      this.reviews = data;
  });
    this._login.getDiscussion(this.username).subscribe(data => {
      console.log(data);
      this.discussions = data;
  });
  }

}
