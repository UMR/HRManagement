import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { LoginModel } from '../common/models/login';
import { LoginService } from './login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public loginModel: LoginModel[] = [];
  public lsIsLoggedin: string = 'not logged in';
  public isLoading: boolean = false;
  public loginForm: any;
  public submitted = false;
  private emailRegEx = '^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$';

  constructor(private router: Router, private formBuilder: FormBuilder,
    private loginService: LoginService,
    private messageService: MessageService) { }

  ngOnInit(): void {
    localStorage.clear();
    this.loginForm = this.formBuilder.group({
      email: new FormControl(null, [Validators.required, Validators.pattern(this.emailRegEx)]),
      password: new FormControl(null, [Validators.required, Validators.minLength(6)])
    });
  }

  get email() { return this.loginForm.get('email'); }
  get password() { return this.loginForm.get('password'); }

  goToPage(pageName: any): void {
    this.router.navigate([`${pageName}`]);
  }

  onSubmit() {
    this.lsIsLoggedin = 'logged in';
    localStorage.setItem('loggedinData', this.lsIsLoggedin);
    this.goToPage('');
    this.submitted = true;

    if (!this.loginForm.invalid) {
      this.isLoading = true;
      this.messageService.clear();
      this.loginModel = [];
      this.loginModel.push({
        Email: this.loginForm.controls['email'].value,
        Password: this.loginForm.controls['password'].value
      })

      this.loginService.login(this.loginModel[0])
        .subscribe(data => {
          this.messageService.add({ key: 'toastKey1', severity: 'success', summary: 'Login Successful', detail: '' });
        },
          err => {
            this.isLoading = false;
            this.messageService.add({ key: 'toastKey1', severity: 'error', summary: 'Login failed', detail: '' });
          },
          () => {
            this.isLoading = false;
          });
    }
  }
}
