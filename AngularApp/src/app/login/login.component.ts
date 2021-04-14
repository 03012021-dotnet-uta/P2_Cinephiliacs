import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { LoginService } from '../login.service';
import { User } from '../models';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  @Input() currentUser: User = {
    username:'',
    firstname:'',
    lastname:'',
    email:'',
    permissions:1
  }
  @Output() currentUserChange = new EventEmitter<User>();

  userName: string = "";

  isLoginPage: boolean = true;

  newUser: any = {
    username:'',
    firstname:'',
    lastname:'',
    email:'',
    permissions:1
  }

  constructor(private _login: LoginService) { }

  ngOnInit(): void {
  }

  login(){
    console.log("Login attempt" + this.userName);
    this._login.loginUser(this.userName).subscribe((data: User) => {
      console.log(data);
      this.currentUser = data;
      console.log(this.currentUser.username);
      this.currentUserChange.emit(this.currentUser);
      localStorage.setItem("loggedin",JSON.stringify(this.currentUser));
      return data;
    });
  }

  createUser(): void {
    console.log("In Create");
    if(!this.newUser.firstname || !this.newUser.lastname || !this.newUser.username ||!this.newUser.email)
    {
      console.log("Please fill in the correct data")
    }
    else
    {
      this.newUser
      console.log(JSON.stringify(this.newUser));
      this._login.createUser(this.newUser).subscribe(data => {
        console.log(data);
        this.currentUser = this.newUser;
        this.isLoginPage = true;
      });
    }
  }

  switchToRegister(): void {
    this.isLoginPage = false;
  }

  backToLogin(): void {
    this.isLoginPage = true;
  }
}