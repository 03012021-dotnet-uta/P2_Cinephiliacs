import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { FormsModule} from '@angular/forms';


@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(private http: HttpClient, private router: Router) { 
    
  }

  getMovies(searchTerm: string, pageNum: number ){
    return this.http.get("https://movie-database-imdb-alternative.p.rapidapi.com/?rapidapi-key=f6d24cec46msh55a535ae1cf36d0p1bf4bcjsnd49b624447d2&s=" + searchTerm +"&r=json&page=" + pageNum);
    //return this.http.get('https://api.openbrewerydb.org/breweries')
    //COME ON WORK
  }

  getMovie(id: string){
    return this.http.get("https://movie-database-imdb-alternative.p.rapidapi.com?rapidapi-key=f6d24cec46msh55a535ae1cf36d0p1bf4bcjsnd49b624447d2&i="+ id + "&r=json");
  }

  onNavigate(){
    window.open("https://www.google.com", "_blank");
  } 
  

  
}
