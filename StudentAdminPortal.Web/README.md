### Observações:
Cuidado ao usar Paginator com If
ViewChild, precisa do HTML construido para pegar o elemento da DOM
EX:
   @ViewChild(MatPaginator, {static: false}) paginator!: MatPaginator;
   @ViewChild(MatSort) matSort!: MatSort;

   ngAfterViewInit(): void {
    if (this.paginator) {
      this.dataSource.paginator = this.paginator;
    }

    if(this.matSort) {
      this.dataSource.sort = this.matSort;
    }
  }
