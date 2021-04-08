import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HttpService } from './http.service';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  currentUser:string ="";
  askingUser:string = "";
  connection:string ="";

  constructor(private http: HttpClient, private _http: HttpService) { }

  loginUser(username: string){
    this.connection = this._http.getBase() +"user/" + username;
    console.log(this.connection);
     this.http.get(this.connection).subscribe(data => {
      console.log(data);
      return data;
    });;
  }

  createUser(newUser: any){
    console.log("User" + newUser);

    
    this.http.post(this._http.getBase() , newUser).subscribe(data => console.log(data));
  }
}
