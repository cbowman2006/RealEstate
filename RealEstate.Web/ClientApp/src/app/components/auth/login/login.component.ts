import { ActivatedRoute, Router } from '@angular/router';
import { AlertifyService } from './../../../services/alertify.service';
import { AuthService } from './../../../services/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  model: any = {};
  userId: number;
  constructor(public authService: AuthService, private alertify: AlertifyService,
    private router: Router) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      this.alertify.success('Logged in sucessfully');
    }, error => {
      this.alertify.error(error);
    },() => {
      this.userId = this.authService.decodedToken.nameid;
      this.router.navigate(['/home']);
    });
  }
}
