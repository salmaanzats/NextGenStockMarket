import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BankTransactionComponent } from './bank-transaction.component';

describe('BankTransactionComponent', () => {
  let component: BankTransactionComponent;
  let fixture: ComponentFixture<BankTransactionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BankTransactionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BankTransactionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
