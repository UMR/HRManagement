import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { RegistrationModel } from '../common/models/registration';
import { RegistrationService } from './registration.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  public regModel: RegistrationModel[] = [];
  public isLoading: boolean = false;
  public registerForm: any;
  public submitted = false;
  private emailRegEx = '^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$';

  constructor(private router: Router, private formBuilder: FormBuilder,
    private regService: RegistrationService,
    private messageService: MessageService) { }

  ngOnInit(): void {
    this.registerForm = this.formBuilder.group({
      firstname: new FormControl(null, [Validators.required, Validators.pattern('[a-zA-Z ]*')]),
      lastname: new FormControl(null, [Validators.required, Validators.pattern('[a-zA-Z ]*')]),
      email: new FormControl(null, [Validators.required, Validators.pattern(this.emailRegEx)]),
      password: new FormControl(null, [Validators.required, Validators.minLength(6)]),
      confirmPassword: new FormControl(null, [Validators.required])
    },
    {
      validators: this.Mismatch('password', 'confirmPassword')
    });
  }

  get firstname() { return this.registerForm.get('firstname'); }
  get lastname() { return this.registerForm.get('lastname'); }
  get email() { return this.registerForm.get('email'); }
  get password() { return this.registerForm.get('password'); }
  get confirmPassword() { return this.registerForm.get('confirmPassword'); }

  Mismatch(controlName: string, matchingControlName: string) {
    return (formGroup: FormGroup) => {
      const control = formGroup.controls[controlName];
      const matchingControl = formGroup.controls[matchingControlName];

      if (matchingControl.errors && !matchingControl.errors['Mismatch']) {
        return
      }

      if (control.value !== matchingControl.value) {
        matchingControl.setErrors({ Mismatch : true});
      }
      else {
        matchingControl.setErrors(null);
      }
    }
  }

  goToPage(pageName: any): void {
    this.router.navigate([`${pageName}`]);
  }

  submitForm() {
    this.submitted = true;
    if (!this.registerForm.invalid) {
      this.isLoading = true;
      this.messageService.clear();
      this.regModel = [];
      this.regModel.push({
        FirstName: this.registerForm.controls['firstname'].value,
        LastName: this.registerForm.controls['lastname'].value,
        Email: this.registerForm.controls['email'].value,
        Password: this.registerForm.controls['password'].value
      })
      this.regService.registration(this.regModel[0])
        .subscribe(data => {
          this.registerForm.reset();
          this.messageService.add({ key: 'toastKey1', severity: 'success', summary: 'Registration Successful', detail: '' });
        },
          err => {
            this.isLoading = false;
            this.messageService.add({ key: 'toastKey1', severity: 'error', summary: 'Registration failed', detail: '' });
          },
          () => {
            this.isLoading = false;
          });
    }
  }
}
