import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddIcuComponent } from './add-icu.component';

describe('AddIcuComponent', () => {
  let component: AddIcuComponent;
  let fixture: ComponentFixture<AddIcuComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddIcuComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddIcuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
