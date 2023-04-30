import { StudentService } from './../../core/services/student/student.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ThemePalette } from '@angular/material/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AppStrings } from 'src/app/core/constants/appString';
import StudentViewModel from 'src/app/core/models/student.model';

@Component({
  selector: 'app-list-students',
  templateUrl: './list-students.component.html',
  styleUrls: ['./list-students.component.scss']
})
export class ListStudentsComponent implements OnInit {
  studentList: StudentViewModel[] = [];
  studentName: string = '';
  labelInput: string = AppStrings.searchStudentByName;
  filter: string= '';
  displayedColumns: string[] = ['firstName', 'lastName', 'dateOfBirth', 'email', 'mobile', 'gender'];
  dataSource: MatTableDataSource<StudentViewModel> = new MatTableDataSource<StudentViewModel>();
  countResults: number = 0;
  isLoadingResult: boolean = true;
  isInvalidInputValue: boolean = true;
  colorLabelInput: ThemePalette = "accent"

  @ViewChild(MatPaginator) set paginator(value: MatPaginator) {
    if(this.dataSource) {
      this.dataSource.paginator = value;
    }
  }
  @ViewChild(MatSort) set sort(value: MatSort) {
    if(this.dataSource) {
      this.dataSource.sort = value;
    }
  }

  constructor( private studentService: StudentService) {
  }

  ngOnInit(): void {
    this.getAllStudent();
  }

  getAllStudent() {
    this.studentService.getAllStudents<StudentViewModel[]>().subscribe({
      next: next => {
        this.dataSource.data = next;
        this.countResults = next.length;
      },
      complete: () => { this.isLoadingResult = false }
    })
  }

  getStudentsByName(name: string): void {
    this.isLoadingResult = true;
    this.studentService.getStudentByName<StudentViewModel[]>(name).subscribe({
      next: next => {
        this.dataSource.data = next;
      },
      complete: () => { this.isLoadingResult = false }
    })
  }

  searchStudent(event: string){
    event.trim();
    if (event.length > 0 && event.length < 3) {
      this.isInvalidInputValue = true;
      this.labelInput = AppStrings.labelExceptionMessage
      this.colorLabelInput = "warn"
      return;
    }
    this.getStudentsByName(event);
  }

  filterStudents() {
    this.dataSource.filter = this.filter;
  }
}
