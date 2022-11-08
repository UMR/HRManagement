import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {

  forgotPasswordForm: any;

  constructor(private router: Router, private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.forgotPasswordForm = this.formBuilder.group({
      code: new FormControl(null, [Validators.required]),
      password: new FormControl(null, [Validators.required, Validators.minLength(6)]),
      confirmPassword: new FormControl(null, [Validators.required])
    },
      {
        validators: this.Mismatch('password', 'confirmPassword')
      });
  }

  get code() { return this.forgotPasswordForm.get('code'); }
  get password() { return this.forgotPasswordForm.get('password'); }
  get confirmPassword() { return this.forgotPasswordForm.get('confirmPassword'); }

  Mismatch(controlName: string, matchingControlName: string) {
    return (formGroup: FormGroup) => {
      const control = formGroup.controls[controlName];
      const matchingControl = formGroup.controls[matchingControlName];

      if (matchingControl.errors && !matchingControl.errors['Mismatch']) {
        return
      }

      if (control.value !== matchingControl.value) {
        matchingControl.setErrors({ Mismatch: true });
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
    console.log(this.forgotPasswordForm.value);

    if (this.forgotPasswordForm.valid) {
      alert(`Password has been set.`);
      this.forgotPasswordForm.reset();
    }
  }

}
