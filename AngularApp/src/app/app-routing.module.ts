import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ListComponent } from './list/list.component';
import { MovieComponent } from './movie/movie.component';



const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'list/:search/:page', component: ListComponent},
  {path: 'movie/:id', component: MovieComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
