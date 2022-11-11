import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  public loggedin: boolean = false;

  constructor(private router: Router) { }

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
