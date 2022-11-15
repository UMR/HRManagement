import { AfterViewInit, Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { MessageService } from 'primeng/api';
import { FileUpload } from 'primeng/fileupload';
import { Observable } from 'rxjs';
import { DomSanitizer } from '@angular/platform-browser';
import { FileService } from '../services/file.service';

@Component({
  selector: 'app-user-information',
  templateUrl: './user-information.component.html',
  styleUrls: ['./user-information.component.css']
})
export class UserInformationComponent implements OnInit, AfterViewInit {
  @Output() imgfileChange = new EventEmitter<string>();
  @ViewChild('fileInput') fileInput!: FileUpload;
  public uploadedFiles: any[] = [];
  public base64: any = '';
  public file: any = '';
  public fileData: string = "";
  public fileName: string = "";
  //public hasFileData: string = 'no';

  constructor(private messageService: MessageService, private _sanitizer: DomSanitizer, private fileService: FileService) { }
  
  ngOnInit(): void{
    localStorage.removeItem("hasFileData");
  }

  onUpload(event: any) {
   /* for (let file of event.files) {
      this.uploadedFiles.push(file);
    }*/

    let reader = new FileReader();
    reader.readAsDataURL(event.files[0]);
    reader.onload = () => {
      this.base64 = reader.result?.toString().split(',')[1] || '';

      this.file = this._sanitizer.bypassSecurityTrustResourceUrl('data:image/*;base64,' + this.base64);

      localStorage.setItem('fileData', this.file);
      localStorage.setItem('hasFileData', 'yes');

      this.imgfileChange.emit(this.file);

      this.fileService.setFile(this.file);
      // console.log(this.base64);
      // console.log(this.file);

      //console.log(event.files[0].name);
    };
    reader.onerror = (error) => {
      console.log('Error: ', error);
    };


    this.messageService.add({ severity: 'info', summary: 'File Uploaded', detail: '' });
  }

  ngAfterViewInit() {
    this.fileInput.files.push();
  }

}
