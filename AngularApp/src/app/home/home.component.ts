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

  _login: LoginService = new LoginService();

  constructor( private _http: HttpClient) { }

  ngOnInit(): void {
  }

  async login(){
    console.log("Login attempt" + this.userName);
    this.url =  this._login.getURL() +"user/" + this.userName;
    console.log(this.url);
    await this._http.get(this.url).subscribe(data => {
      console.log(data);
      this.loggedIn = data;
      console.log(this.loggedIn.username);
      localStorage.setItem("loggedin",this.loggedIn.userName);
      return data;
    });;
  }

  createUser(){
    console.log("In Create");
    if(!this.newUser.firstname || !this.newUser.lastname || !this.newUser.username ||!this.newUser.email)
    {
      console.log("Please fill in the correct data")
    }else{
    this.newUser 
    console.log(JSON.stringify(this.newUser));
    this._http.post(this._login.getURL()+ "user/",this.newUser).subscribe(data => console.log("Data" + data));
  }
  }

  logout(){
    localStorage.clear();
  }
  
  
}
