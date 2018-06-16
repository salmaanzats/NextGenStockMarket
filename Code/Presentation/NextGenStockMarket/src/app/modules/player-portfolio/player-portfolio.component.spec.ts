import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlayerPortfolioComponent } from './player-portfolio.component';

describe('PlayerPortfolioComponent', () => {
  let component: PlayerPortfolioComponent;
  let fixture: ComponentFixture<PlayerPortfolioComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlayerPortfolioComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlayerPortfolioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
