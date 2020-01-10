import { AuthComponent } from './components/auth/auth.component';
import { HomeComponent } from './components/home/home.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserProfileComponent } from './components/user/user-profile/user-profile.component';


const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'login', component: AuthComponent},
  {path: 'register', component: AuthComponent},
  {path: 'profile/:id', component: UserProfileComponent},
  {path:'**', redirectTo:'', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
