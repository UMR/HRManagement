import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {
  changePasswordForm: any;

  constructor(private router: Router, private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.changePasswordForm = this.formBuilder.group({
      oldPassword: new FormControl(null, [Validators.required, Validators.minLength(6)]),
      password: new FormControl(null, [Validators.required, Validators.minLength(6)]),
      confirmPassword: new FormControl(null, [Validators.required])
    },
      {
        validators: this.Mismatch('password', 'confirmPassword')
      });
  }

  get oldPassword() { return this.changePasswordForm.get('oldPassword'); }
  get password() { return this.changePasswordForm.get('password'); }
  get confirmPassword() { return this.changePasswordForm.get('confirmPassword'); }

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
    console.log(this.changePasswordForm.value);

    if (this.changePasswordForm.valid) {
      alert(`Password has been set.`);
      this.changePasswordForm.reset();
    }
  }

}
