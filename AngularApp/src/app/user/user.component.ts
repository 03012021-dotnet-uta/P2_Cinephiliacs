import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {

  router :ActivatedRoute = new ActivatedRoute();

  username: string = "";
  user: any;
  reviews: any;
  discussions: any;
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.username = this.router.snapshot.params.username;
    console.log(this.username);
    this.http.get("https://cinephiliacsapi.azurewebsites.net/user/"+ this.username).subscribe(data => {
      console.log(data);
      this.user = data;
  });
    this.http.get("https://cinephiliacsapi.azurewebsites.net/user/reviews/" + this.username).subscribe(data => {
      console.log(data);
      this.reviews = data;
  });
    this.http.get("https://cinephiliacsapi.azurewebsites.net/user/discussions/" + this.username).subscribe(data => {
      console.log(data);
      this.discussions = data;
  });
  }

}
