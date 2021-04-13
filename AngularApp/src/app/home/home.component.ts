import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { LoginService } from '../login.service';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  userName:string = "";

  url:string ="";

  newUser:any ={
    username:'',
    firstname:'',
    lastname:'',
    email:'',
    permissions:1
  }

  loggedIn: any;

  constructor( private _login: LoginService) { }

  ngOnInit(): void {
  }

  login(){
    console.log("Login attempt" + this.userName);
    this._login.loginUser(this.userName).subscribe(data => {
      console.log(data);
      this.loggedIn = data;
      console.log(this.loggedIn.username);
      localStorage.setItem("loggedin",this.loggedIn.userName);
      return data;
    });
  }

  createUser(){
    console.log("In Create");
    if(!this.newUser.firstname || !this.newUser.lastname || !this.newUser.username ||!this.newUser.email)
    {
      console.log("Please fill in the correct data")
    }else{
    this.newUser 
    console.log(JSON.stringify(this.newUser));
    this._login.createUser(this.newUser).subscribe(data => console.log( data));
  }
  }

  logout(){
    localStorage.clear();
  }
  
  
}
