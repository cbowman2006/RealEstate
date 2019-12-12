import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BsDropdownModule, TabsModule, BsDatepickerModule, PaginationModule, ButtonsModule } from 'ngx-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import{ JwtModule } from '@auth0/angular-jwt';
import {MatButtonModule} from '@angular/material/button';
import {MatMenuModule} from '@angular/material/menu';


import { AuthComponent } from './components/auth/auth.component';
import { HomeComponent } from './components/home/home.component';
import { FooterComponent } from './components/shared/footer/footer.component';
import { LoginComponent } from './components/auth/login/login.component';
import { NavBarComponent } from './components/shared/nav-bar/nav-bar.component';
import { RegisterComponent } from './components/auth/register/register.component';


//Services
import { AlertifyService } from './services/alertify.service';
import { AuthService } from './services/auth.service';

export function tokenGetter(){
  return localStorage.getItem('token');
}



@NgModule({
  declarations: [
    AppComponent,
    AuthComponent,
    HomeComponent,
    FooterComponent,
    LoginComponent,
    NavBarComponent,
    RegisterComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    BsDropdownModule.forRoot(),
    ButtonsModule.forRoot(),
    FormsModule,
    HttpClientModule,
    MatButtonModule,
    MatMenuModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        whitelistedDomains: ['localhost:5000'],
        blacklistedRoutes: ['localhost:5000/api/auth']
      }
    }),
    ReactiveFormsModule
  ],
  providers: [
    AlertifyService,
    AuthService, 
    
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
