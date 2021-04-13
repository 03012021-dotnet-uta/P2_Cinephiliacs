import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { AppComponent } from './app.component';


@Injectable({
  providedIn: 'root'
})
export class LoginService {

  currentUser:string ="";
  askingUser:string = "";
  connection:string ="";
  loggedIn:any;

  baseURL:string = "https://cinephiliacsapi.azurewebsites.net/";

  constructor(private http:HttpClient) { }

  createUser(newUser:string){
    return this.http.post(this.baseURL+ "user/",newUser);
  }

  loginUser(userName:string){
    this.connection =  this.baseURL +"user/" + userName;
    console.log(this.connection);
    return this.http.get(this.connection);
  }

  getURL(){
    return this.baseURL;
  }

}
