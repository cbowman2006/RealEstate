import { AuthService } from './../../../services/auth.service';
import { UserService } from './../../../services/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from './../../../models/User';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {
user: User;
userId : number;

  constructor(private route: ActivatedRoute, private router: Router, private userService: UserService, private authService: AuthService) {}

  ngOnInit() {
    this.userService.getUser(this.authService.decodedToken.nameid).subscribe(user => this.user = user);
  }

}
