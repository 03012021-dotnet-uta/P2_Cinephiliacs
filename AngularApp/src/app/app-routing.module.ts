import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AllUsersComponent } from './all-users/all-users.component';
import { DiscussionComponent } from './discussion/discussion.component';
import { HomeComponent } from './home/home.component';
import { ListComponent } from './list/list.component';
import { MovieComponent } from './movie/movie.component';
import { UserComponent } from './user/user.component';



const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'list/:search/:page', component: ListComponent},
  {path: 'movie/:id', component: MovieComponent},
  {path: 'alluser', component: AllUsersComponent},
  {path: 'user/:username',component:UserComponent},
  {path: 'discussion/:id',component:DiscussionComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
