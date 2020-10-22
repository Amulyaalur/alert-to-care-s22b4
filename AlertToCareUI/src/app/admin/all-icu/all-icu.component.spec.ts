import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AllIcuComponent } from './all-icu.component';

describe('AllIcuComponent', () => {
  let component: AllIcuComponent;
  let fixture: ComponentFixture<AllIcuComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AllIcuComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AllIcuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
