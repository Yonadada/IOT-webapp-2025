using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WpfTodoListApp.Models;

namespace WpfTodoListApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : MetroWindow
{
    HttpClient client = new HttpClient();
    //ObservableCollection을  굳이 사용하지 않아도 기능에는 문제 없음
    TodoItemsCollection todoItems = new TodoItemsCollection();


    public MainWindow()
    {
        InitializeComponent();
    }

    private void MetroWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        var comboPairs= new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>("완료", 1),
            new KeyValuePair<string, int>("미완료", 0)
        };

        CboIsComplete.ItemsSource = comboPairs; // 콤보박스에 데이터 바인딩


        //Rest API 호출 준비
        client.BaseAddress = new System.Uri("http://localhost:6200"); //api 서버 URL
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        // 데이터 가졍;ㅗ기
        GetDatas();
    }

    private async Task GetDatas()
    {
        // api/TodoItems Get 메서드 호출
        GrdTodoItems.ItemsSource = todoItems;

        try // api 호출
        {
            //http://localhost:6200/api/TodoItems
            HttpResponseMessage? response = await client.GetAsync("/api/TodoItems");
            response.EnsureSuccessStatusCode(); // 상태코드 확인 

            var items = await response.Content.ReadAsAsync<IEnumerable<TodoItem>>();
            todoItems.CopyFrom(items); // observableCollection으로 형변환

            // 성공메시지
            await this.ShowMessageAsync("API 호출", "데이터를 성공적으로 가져왔습니다.");
          
        }
        catch (Exception ex)
        {
            // 예외메세지
            await this.ShowMessageAsync("error", ex.Message);
              
        }
    }

    //async 시 Task 가 리턴값이지만 버튼클릭 이벤트 메서드와 연결시키는 Task -> void 로 변경해줘야 연결 가능
    private async void BtnInsert_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        try
        {
            var todoItem = new TodoItem
            {
                Id = 0, // Id는 서버에서 자동으로 생성됨(AUtoIncrement)
                Title = TxtTitle.Text,
                TodoDate = Convert.ToDateTime(DtpTodoDate.SelectedDate).ToString("yyyyMMdd"), //20250610
                IsCompleted = Convert.ToBoolean(CboIsComplete.SelectedValue) // 0,1
            };
            //데이터 입력확인
            //Debug.WriteLine(todoItem.Title);

            //POST 메서드 API 호출
            var response = await client.PostAsJsonAsync("/api/TodoItems", todoItem);
            response.EnsureSuccessStatusCode(); // 상태코드 확인

            GetDatas(); // 데이터 새로고침
            initControls();
        }
        catch (Exception ex)
        {
            // 예외메세지
            await this.ShowMessageAsync("API 호출 에러", ex.Message);
        }
    }

    private async void GrdTodoItems_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        try
        {
            //await this.ShowMessageAsync("클릭", "클릭확인");
            var Id = (GrdTodoItems.SelectedItem as TodoItem)?.Id; // NUll일 수 있으므로 ? 사용, null 생겨도 예외발생 안 함

            if (Id == null) return; // 선택된 아이템이 없으면 리턴

            // api/TodoItems/{Id} Get 메서드 API 호출
            var response = await client.GetAsync($"/api/TodoItems/{Id}");
            response.EnsureSuccessStatusCode(); // 상태코드 확인

            var item = await response.Content.ReadAsAsync<TodoItem>();

            TxtId.Text = item.Id.ToString();
            TxtTitle.Text = item.Title.ToString();
            DtpTodoDate.SelectedDate = DateTime.Parse(item.TodoDate.Insert(4, "-").Insert(7, "-"));
            CboIsComplete.SelectedValue = item.IsCompleted;
        }
        catch (Exception ex)
        {
            // 예외메세지
            await this.ShowMessageAsync("API 호출 에러", ex.Message);
        }
    }

    private async void BtnUpdate_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        try
        {
            var todoItem = new TodoItem
            {
                Id = Convert.ToInt32(TxtId.Text),
                Title = TxtTitle.Text,
                TodoDate = Convert.ToDateTime(DtpTodoDate.SelectedDate).ToString("yyyyMMdd"), //20250610
                IsCompleted = Convert.ToBoolean(CboIsComplete.SelectedValue) // 0,1
            };

            var response = await client.PutAsJsonAsync($"/api/TodoItems/{todoItem.Id}", todoItem);
            response.EnsureSuccessStatusCode(); // 상태코드 확인

            GetDatas(); // 데이터 새로고침

            initControls(); // 입력양식 초기화

        }
        catch (Exception ex)
        {

            // 예외메세지
            await this.ShowMessageAsync("API 호출 에러", ex.Message);
        }
    }

    public void initControls()
    {
        //입력양식 초기화
        TxtId.Text = string.Empty;
        TxtTitle.Text = string.Empty;
        DtpTodoDate.Text = string.Empty;
        CboIsComplete.Text = string.Empty;
    }

    private async void BtnDelete_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        try
        {
            var Id = Convert.ToInt32(TxtId.Text); // 삭제는 Id만 파라미터로 전송

            var response = await client.DeleteAsync($"/api/TodoItems/{Id}");
            response.EnsureSuccessStatusCode(); // 상태코드 확인

            GetDatas();

            initControls();

        }
        catch (Exception ex)
        {

            // 예외메세지
           await this.ShowMessageAsync("API 호출 에러", ex.Message);
        }
    }
}