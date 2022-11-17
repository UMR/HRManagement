import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';

interface leavetype {
  name: string,
  code: string
}

@Component({
  selector: 'app-leave-application',
  templateUrl: './leave-application.component.html',
  styleUrls: ['./leave-application.component.css']
})
export class LeaveApplicationComponent implements OnInit {
  public leaveForm: any;

  public _leavetype: leavetype[] = [];
  leavetype!: string;
  
  constructor(private formBuilder: FormBuilder)
  {
    this._leavetype = [
      { code: '1', name: 'Sick Leave' },
      { code: '2', name: 'Special Leave' },
      { code: '3', name: 'Casual Leave' }
    ];
  }

  ngOnInit(): void {
    this.leaveForm = this.formBuilder.group({
      leaveFrom: new FormControl(null, [Validators.required]),
      leaveTo: new FormControl(null, [Validators.required]), 
      noOfDays: new FormControl(null, [Validators.required, Validators.pattern("^[0-9]*$")]),
      leavetype: new FormControl(null, [Validators.required]),
      contactnumber: new FormControl(null, [Validators.required, Validators.pattern("^[0-9]*$")]),
      reasonforLeave: new FormControl(null),
      approver: new FormControl(null, [Validators.required, Validators.pattern('[a-zA-Z ]*')]),
    });
  }
  get leaveFrom() { return this.leaveForm.get('leaveFrom'); }
  get leaveTo() { return this.leaveForm.get('leaveTo'); }
  get noOfDays() { return this.leaveForm.get('noOfDays'); }
  get contactnumber() { return this.leaveForm.get('contactnumber'); } 
  get reasonforLeave() { return this.leaveForm.get('reasonforLeave'); } 
  get approver() { return this.leaveForm.get('reasonforLeave'); }

  submitForm() {
    console.log(this.leaveForm.value);

    if (this.leaveForm.valid) {
      this.leaveForm.reset();
    }
  }

}
