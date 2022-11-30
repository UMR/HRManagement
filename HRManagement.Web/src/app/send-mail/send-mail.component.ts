import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserInformation } from '../user-information/user-information';


@Component({
  selector: 'app-send-mail',
  templateUrl: './send-mail.component.html',
  styleUrls: ['./send-mail.component.css']
})
export class SendMailComponent implements OnInit {
  sendEmailForm: any;
  public _companyName: UserInformation[] = [];
  companyName!: string
  public _sendingOption: UserInformation[] = [];
  sendingOption!: string;

  constructor(private router: Router, private formBuilder: FormBuilder) {

    this._companyName = [
      { code: '1', name: 'company1' },
      { code: '2', name: 'company2' },
      { code: '3', name: 'company3' },
    ];
    this._sendingOption = [
      { code: '1', name: 'Option1' },
      { code: '2', name: 'Option2' },
      { code: '3', name: 'Option3' },
    ];
  }

  ngOnInit(): void {
    this.sendEmailForm = this.formBuilder.group({
      companyName: new FormControl(null, [Validators.required]),
      sendingOption: new FormControl(null, [Validators.required]),
      mailAddress: new FormControl(null, [Validators.required, Validators.email]),
      password: new FormControl(null, [Validators.required]),
      subject: new FormControl(null, [Validators.required]),
      mailBody: new FormControl(null, [Validators.required]),
    });
  }
  get mailAddress() { return this.sendEmailForm.get('mailAddress'); }
  get password() { return this.sendEmailForm.get('password'); }
  get subject() { return this.sendEmailForm.get('subject'); }
  get mailBody() { return this.sendEmailForm.get('mailBody'); }


  goToPage(pageName: any): void {
    this.router.navigate([`${pageName}`]);
  }

  submitForm() {
    console.log(this.sendEmailForm.value);

    if (this.sendEmailForm.valid) {
      alert(`Email Sent.`);
      this.sendEmailForm.reset();
    }
  }
}
