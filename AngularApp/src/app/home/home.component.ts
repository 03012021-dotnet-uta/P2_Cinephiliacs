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

  newUser:any ={
    username:'',
    firstname:'',
    lastname:'',
    email:'',
    permissions:1
  }

  constructor(private _login: LoginService, private _http: HttpClient) { }

  ngOnInit(): void {
  }

  async login(){
    console.log("Login attempt" + this.userName);
    await this._login.loginUser(this.userName);
  }

  createUser(){
    console.log("In Create");
    if(!this.newUser.firstname || !this.newUser.lastname || !this.newUser.username ||!this.newUser.email)
    {
      console.log("Please fill in the correct data")
    }else{
    this.newUser 
    console.log(JSON.stringify(this.newUser));
    this._http.post("https://cinephiliacsapi.azurewebsites.net/user/",this.newUser).subscribe(data => console.log("Data" + data));
  }
  }

  logout(){
    localStorage.clear();
  }
  
  
}
