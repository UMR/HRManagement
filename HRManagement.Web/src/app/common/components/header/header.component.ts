import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DomSanitizer } from '@angular/platform-browser';
import { FileService } from '../../../services/file.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  public loggedin: boolean = false;
  public defalutImg: any = '../../../../assets/user_icon.png';

  constructor(private router: Router, private _sanitizer: DomSanitizer, private fileService: FileService) {
    
    this.fileService.getFile().subscribe((value: string) => {
      if (value) {

        this.defalutImg = value;
      }

    })
  }

  ngOnInit(): void {
    if (localStorage.getItem('loggedinData') == 'logged in') {
      this.loggedin = true;
    }

    /*var hasData = localStorage.getItem("hasFileData");
    if (hasData == 'yes') {
      this.fileService.getImageFile().subscribe(data => this.defalutImg = data);
    }
    console.log(hasData);
    console.log(this.defalutImg);*/
  }

  imgChanged(file: any) {
    this.defalutImg = file;
    console.log(this.defalutImg);
  }

  /*ngOnChanges(): void {
    var hasData = localStorage.getItem("hasFileData");
    console.log(hasData);
    if (hasData == 'yes') {
      console.log('Hit');
      var data: any = localStorage.getItem("fileData");
      this.defalutImg = data.toString().split('unsafe:SafeValue must use [property]=binding: ')[1]
    }
    console.log(this.defalutImg);
  }*/

  goToPage(pageName: any): void {
    this.router.navigate([`${pageName}`]);
  }

  logout() {
    localStorage.clear();
  }

}
