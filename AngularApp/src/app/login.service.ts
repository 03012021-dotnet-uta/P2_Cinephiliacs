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

  constructor(private http: HttpClient, private _http: HttpService) { }

  async loginUser(username: string){
    this.connection = this._http.getBase() +"user/" + username;
    console.log(this.connection);
    await this.http.get(this.connection).subscribe(data => {
      console.log(data);
      this.loggedIn = data;
      console.log(this.loggedIn.username);
      
      return data;
    });;
  }

  createUser(newUser: any){
    console.log("User" + newUser);

    
    this.http.post(this._http.getBase() , newUser).subscribe(data => console.log(data));
  }
}
