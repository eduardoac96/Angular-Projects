import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanyBankComponent } from './company-bank.component';

describe('CompanyBankComponent', () => {
  let component: CompanyBankComponent;
  let fixture: ComponentFixture<CompanyBankComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CompanyBankComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CompanyBankComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
