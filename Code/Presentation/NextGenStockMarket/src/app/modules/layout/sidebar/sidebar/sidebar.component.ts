import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Route } from '@angular/compiler/src/core';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {

  player: string;

  constructor(private activatedRoute: ActivatedRoute,private router:Router) { }

  ngOnInit() {
    this.activatedRoute.params.subscribe((params: Params) => {
      this.player = params['player'];
    });

  }
  goToHome(){
    this.router.navigate(['/game/',this.player]);
  }

  goToBankTransactions() {
    this.router.navigate(['/bank/',this.player]);
  }

  goToPlayerPortFolio() {
    this.router.navigate(['/portfolio/',this.player]);
  }
  goToAnalyst(){
    this.router.navigate(['/analyst/',this.player]);
  }
}
