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
  public lsIsLoggedin: string = 'not logged in';

  constructor(private router: Router) { }

  ngOnInit(): void {
    localStorage.clear();
  }

  onSubmit() {
    this.lsIsLoggedin = 'logged in';
    localStorage.setItem('loggedinData', this.lsIsLoggedin);
    /*
    alert(
      `${this.userModal.emailId} Logged in Successfully!!`
    );*/
    this.goToPage('');
  }

  goToPage(pageName: any): void {
    this.router.navigate([`${pageName}`]);
  }

}
