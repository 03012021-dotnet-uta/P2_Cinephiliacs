import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from '../models'

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  currentUser: User = {
    username:'',
    firstname:'',
    lastname:'',
    email:'',
    permissions:1
  }

  constructor( ) { }

  ngOnInit(): void {

    let userData = localStorage.getItem("loggedin");
    if(userData != null) {
      this.currentUser = JSON.parse(userData);
    }
  }

  logout(){
    this.currentUser.username = "";
    this.currentUser.firstname = "";
    this.currentUser.lastname = "";
    this.currentUser.email = "";
    localStorage.clear();
  }


}
