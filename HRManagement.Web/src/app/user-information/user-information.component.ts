import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { DomSanitizer } from '@angular/platform-browser';
import { FileService } from '../services/file.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { UserInformation } from './user-information';

@Component({
  selector: 'app-user-information',
  templateUrl: './user-information.component.html',
  styleUrls: ['./user-information.component.css']
})
export class UserInformationComponent implements OnInit {
  public base64: any = '';
  public file: any = '../../../../assets/user_icon.png';
  public profileEditForm: any;
  public editMode: boolean = false;

  public _gender: UserInformation[] = [];
  public _bloodgroup: UserInformation[] = [];
  public _maritalstatus: UserInformation[] = [];
  public _company: UserInformation[] = [];
  gender!: string;
  bloodgroup!: string;
  maritalstatus!: string;
  company!: string;

  constructor(
    private messageService: MessageService,
    private _sanitizer: DomSanitizer,
    private fileService: FileService,
    private formBuilder: FormBuilder)
  {
    this._gender = [
      { code: 'male', name: 'Male' },
      { code: 'female', name: 'Female' }
    ];
    this._bloodgroup = [
      { code: 'a+', name: 'A positive' },
      { code: 'a-', name: 'A negative' },
      { code: 'b+', name: 'B positive' },
      { code: 'b-', name: 'B negative' },
      { code: 'ab+', name: 'AB positive' },
      { code: 'ab-', name: 'AB negative' },
      { code: 'o+', name: 'O positive' },
      { code: 'o-', name: 'O negative' },
    ];
    this._maritalstatus = [
      { code: 'unmarried', name: 'Unmarried' },
      { code: 'married', name: 'Married' }
    ];
    this._company = [
      { code: 'ael', name: 'AEL' },
      { code: 'dcl', name: 'DCL' },
      { code: 'celloscope', name: 'CELL' }
    ];
  }
  
  ngOnInit(): void{
    localStorage.removeItem("hasFileData");

    this.profileEditForm = this.formBuilder.group({
      firstname: new FormControl(null, [Validators.required, Validators.pattern('[a-zA-Z ]*')]),
      lastname: new FormControl(null, [Validators.required, Validators.pattern('[a-zA-Z ]*')]),
      gender: new FormControl(null, [Validators.required]),
      address: new FormControl(null, [Validators.required]),
      district: new FormControl(null, [Validators.required]),
      division: new FormControl(null, [Validators.required]),
      postalcode: new FormControl(null, [Validators.required]),
      mobile: new FormControl(null, [Validators.required]),
      dob: new FormControl(null, [Validators.required]),
      bloodgroup: new FormControl(null, [Validators.required]),
      maritalstatus: new FormControl(null, [Validators.required]),
      height: new FormControl(null, [Validators.required]),
      weight: new FormControl(null, [Validators.required]),
      placeofbirth: new FormControl(null, [Validators.required]),
      nid: new FormControl(null, [Validators.required]),
      passport: new FormControl(null, [Validators.required]),
      driverlicense: new FormControl(null, [Validators.required]),
      educationalqualification: new FormControl(null, [Validators.required]),
      company: new FormControl(null, [Validators.required]),
      joiningdate: new FormControl(null, [Validators.required]),

      linkedinaccount: new FormControl(null, [Validators.required]),
      gmail: new FormControl(null, [Validators.required]),
      skype: new FormControl(null, [Validators.required]),
      gitaccount: new FormControl(null, [Validators.required]),
    });
  }
  get firstname() { return this.profileEditForm.get('firstname'); }
  get lastname() { return this.profileEditForm.get('lastname'); }
  //get gender() { return this.profileEditForm.get('gender'); }
  get address() { return this.profileEditForm.get('address'); }
  get district() { return this.profileEditForm.get('district'); }
  get division() { return this.profileEditForm.get('division'); }
  get postalcode() { return this.profileEditForm.get('postalcode'); }
  get mobile() { return this.profileEditForm.get('mobile'); }
  get dob() { return this.profileEditForm.get('dob'); }
  //get bloodgroup() { return this.profileEditForm.get('bloodgroup'); }
  //get maritalstatus() { return this.profileEditForm.get('maritalstatus'); }
  get height() { return this.profileEditForm.get('height'); }
  get weight() { return this.profileEditForm.get('weight'); }
  get placeofbirth() { return this.profileEditForm.get('placeofbirth'); }
  get nid() { return this.profileEditForm.get('nid'); }
  get passport() { return this.profileEditForm.get('passport'); }
  get driverlicense() { return this.profileEditForm.get('driverlicense'); }
  get educationalqualification() { return this.profileEditForm.get('educationalqualification'); }
  //get company() { return this.profileEditForm.get('company'); }
  get joiningdate() { return this.profileEditForm.get('joiningdate'); }

  get linkedinaccount() { return this.profileEditForm.get('linkedinaccount'); }
  get gmail() { return this.profileEditForm.get('gmail'); }
  get skype() { return this.profileEditForm.get('skype'); }
  get gitaccount() { return this.profileEditForm.get('gitaccount'); }

  submitForm() {
    console.log(this.profileEditForm.value);

    if (this.profileEditForm.valid) {
      this.profileEditForm.reset();
    }
  }

  editProfile() {
    if (this.editMode == false) {
      this.editMode = true;
    }
    else {
      this.editMode = false;
    }
  }

  onUpload(event: any) {

    let reader = new FileReader();
    reader.readAsDataURL(event.files[0]);
    reader.onload = () => {
      this.base64 = reader.result?.toString().split(',')[1] || '';

      this.file = this._sanitizer.bypassSecurityTrustResourceUrl('data:image/*;base64,' + this.base64);

      this.fileService.setFile(this.file);
    };
    reader.onerror = (error) => {
      console.log('Error: ', error);
    };
                            
    this.messageService.add({ severity: 'info', summary: 'File Uploaded', detail: '' });
  }

  onFileSelect(event: any) {
    if (event.files.length > 0) {
      if (!event.files[0].type.includes("image/") ) {
        this.messageService.add({ key: 'toastKey1', severity: 'error', summary: 'Invalid file type', detail: 'Upload file' });
      } else if (event.files[0].size > 5000000) {
        this.messageService.add({ key: 'toastKey1', severity: 'error', summary: 'Invalid file size', detail: 'File size limit: 5MB' });
      }
    }
  }

}
