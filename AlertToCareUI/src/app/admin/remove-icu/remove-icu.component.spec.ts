import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RemoveIcuComponent } from './remove-icu.component';

describe('RemoveIcuComponent', () => {
  let component: RemoveIcuComponent;
  let fixture: ComponentFixture<RemoveIcuComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RemoveIcuComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RemoveIcuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
