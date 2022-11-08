import { Component, OnInit } from '@angular/core';
import { ValidationErrors } from '@angular/forms';
import { Router } from '@angular/router';
import { Login } from './login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  userModal = new Login();

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  onSubmit() {
    alert(
      `${this.userModal.emailId} Logged in Successfully!!`
    );
    console.log(this.userModal);
  }

  goToPage(pageName: any): void {
    this.router.navigate([`${pageName}`]);
  }

}
