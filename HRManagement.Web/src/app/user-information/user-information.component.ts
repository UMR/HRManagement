import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { DomSanitizer } from '@angular/platform-browser';
import { FileService } from '../services/file.service';

@Component({
  selector: 'app-user-information',
  templateUrl: './user-information.component.html',
  styleUrls: ['./user-information.component.css']
})
export class UserInformationComponent implements OnInit {
  public base64: any = '';
  public file: any = '';

  constructor(private messageService: MessageService,
    private _sanitizer: DomSanitizer, private fileService: FileService) { }
  
  ngOnInit(): void{
    localStorage.removeItem("hasFileData");
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


}
