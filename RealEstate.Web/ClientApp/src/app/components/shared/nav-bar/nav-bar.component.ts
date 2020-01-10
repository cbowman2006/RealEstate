import { User } from './../../../models/User';
import { AlertifyService } from './../../../services/alertify.service';
import { AuthService } from './../../../services/auth.service';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
 userId: number;

  constructor(private authService: AuthService, private alertify: AlertifyService, private router: Router,  private route: ActivatedRoute) {
    route.params.subscribe(p => {
      this.userId = +p['id'];
    })
   }

  ngOnInit() {
    this.userId = this.authService.decodedToken.nameid;
    console.log(this.userId);
  }
  loggedIn(){
    return this.authService.loggedIn();
  }
  logout(){
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.authService.decodedToken = null;
    this.authService.currentUser = null;
    this.alertify.message('Logged out');
    this.router.navigate(['/home']);
   }
}
