import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateIcuComponent } from './update-icu.component';

describe('UpdateIcuComponent', () => {
  let component: UpdateIcuComponent;
  let fixture: ComponentFixture<UpdateIcuComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UpdateIcuComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UpdateIcuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
