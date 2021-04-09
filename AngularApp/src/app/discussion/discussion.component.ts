import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-discussion',
  templateUrl: './discussion.component.html',
  styleUrls: ['./discussion.component.scss']
})
export class DiscussionComponent implements OnInit {

  comments: any;
  disscussionID:number = 0;

  constructor(private http:HttpClient,private router :ActivatedRoute) { }

  ngOnInit(): void {
    this.disscussionID =  this.router.snapshot.params.id;
    this.http.get("https://cinephiliacsapi.azurewebsites.net/forum/comments/" + this.disscussionID).subscribe(data =>{ 
      console.log(data);
      this.comments = data;
    });
  }

}
