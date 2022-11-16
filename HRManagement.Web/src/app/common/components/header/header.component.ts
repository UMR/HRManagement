import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FileService } from '../../../services/file.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  public loggedin: boolean = false;
  public defalutImg: any = '../../../../assets/user_icon.png';

  constructor(private router: Router, private fileService: FileService) {
    
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
  }

  goToPage(pageName: any): void {
    this.router.navigate([`${pageName}`]);
  }

  logout() {
    localStorage.clear();
  }

}
