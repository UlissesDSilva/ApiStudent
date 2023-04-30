import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CadStudentComponent } from './cad-student.component';

describe('CadStudentComponent', () => {
  let component: CadStudentComponent;
  let fixture: ComponentFixture<CadStudentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CadStudentComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CadStudentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
